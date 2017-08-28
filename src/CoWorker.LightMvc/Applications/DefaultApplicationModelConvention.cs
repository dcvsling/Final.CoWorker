
namespace CoWorker.LightMvc.ApplicationParts
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using System.Reflection;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.Options;

    public class DefaultApplicationModelConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            application.Controllers.Each(x => Apply(x));
        }

        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType) return;
            controller.ApiExplorer.GroupName = controller.ControllerName;
            controller.Actions.Each(x => Apply(x));
            var nameitems = controller.ControllerType.Name.Split(new string[] { "Controller" }, StringSplitOptions.RemoveEmptyEntries);
            var route = controller.ControllerType.GenericTypeArguments.FirstOrDefault().Name;
            controller.ControllerName = $"{nameitems.FirstOrDefault()}_{controller.ControllerType.GenericTypeArguments.FirstOrDefault().Name}";
            controller.RouteValues.AddOrUpdate("controller", controller.ControllerName);
            controller.RouteValues.AddOrUpdate("model", controller.ControllerName);
            controller.Selectors.Each(x => x.AttributeRouteModel.Template = "[controller]/[model]");
        }

        public void Apply(ActionModel action)
        {
            action.ApiExplorer.GroupName = action.ActionName;
            action.ActionName = $"{action.ActionName}({action.Parameters.Select(x => x.ParameterName).ToJoin(",")})";
            action.RouteValues.AddOrUpdate("action", action.ActionName);
            action.Selectors.Each(
                x => {
                    x.ActionConstraints.Add(new HttpMethodActionConstraint(new string[] { action.ActionName }));
                    x.AttributeRouteModel.Template = "/";
                });
            action.Parameters.Each(x => Apply(x));
        }

        private void Apply(PropertyModel property)
        {
            property.BindingInfo = new BindingInfo()
            {
                BinderModelName = property.PropertyName,
                BindingSource = BindingSource.Services
            };
        }

        private void Apply(ParameterModel param)
        {
            param.BindingInfo = new BindingInfo();
            param.BindingInfo.BinderModelName = param.ParameterName;
            param.BindingInfo.BindingSource = BindingSource.ModelBinding;
        }
    }
}

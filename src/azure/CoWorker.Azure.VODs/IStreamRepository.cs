using System;
using System.Collections;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoWorker.Models.VODs
{
    public interface IAssetRepository : IAssetStore
    {
        void Add(IAsset asset);

        void Remove(IAsset asset);
    }

    public interface IAssetStore : IEnumerable<IAsset>
    {
        IAsset Find(string id);
    }

    public class AssetRepository : IAssetRepository
    {
        private readonly CloudBaseCollection<IAsset> _cloud;

        public AssetRepository(CloudBaseCollection<IAsset> cloud)
        {
            this._cloud = cloud;
        }

        public void Add(IAsset asset)
            => _cloud.MediaContext.Assets.CreateAsync(,AssetCreationOptions. );
        public IAsset Find(String id) => throw new NotImplementedException();
        public IEnumerator<IAsset> GetEnumerator() => throw new NotImplementedException();
        public void Remove(IAsset asset) => throw new NotImplementedException();
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}

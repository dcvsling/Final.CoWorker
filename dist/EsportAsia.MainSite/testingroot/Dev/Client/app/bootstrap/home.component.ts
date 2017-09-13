//import { Router ,NavigationStart} from '@angular/router';
import { Component } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/toPromise';
@Component({
    selector: 'home',
    templateUrl: 'home.component.html'
})
export class HomeComponent {
    HOST = "https://extreme-esport-website-backend.azurewebsites.net"
    constructor(public http: Http) {

    }
    login() {
        this.postHttp(this.HOST+'/auth/Google',"").toPromise().then(
            res=>{
                console.log(res)
            }
        )
    }
    logout() {
        this.postHttp(this.HOST+'/auth/Logout',"").toPromise().then(
            res=>{
                console.log(res)
            }
        )
    }
    getUser() {
        this.getHttp(this.HOST+'/auth/user').toPromise().then(
            res=>{
                console.log(res)
            }
        )
    }

    getAuth() {
        this.getHttp(this.HOST+'/auth').toPromise().then(
            res=>{
                console.log(res)
            }
        )
    }

    getHttp(dataUrl): Observable<any> {
        console.log('getting',dataUrl)
        return this.http.get(dataUrl)
            // 响应数据是JSON字符串格式的。 我们必须把这个字符串解析成JavaScript对象
            .map((res: Response) => res.json())
            // 异常的捕获并进行处理
            .catch((err) => { throw this.handleError(err) });
    }
    postHttp(dataUrl, data): Observable<any> {
        //let headers = new Headers();
        //this.createAuthorizationHeader(headers);
        console.log('posting',dataUrl)
        return this.http.post(dataUrl, data)
            .map((res: Response) => res.json())
            .catch((err) => { console.error(err); throw this.handleError(err) })
    }
    // 定义私有方法来处理异常
    private handleError(error: any) {
        // 我们的服务处理器(handleError)把响应对象记录到控制台中
        // 把错误转换成对用户友好的消息，并且通过Observable.throw来
        // 把这个消息放进一个新的、用于表示“失败”的可观察对象
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        console.error(errMsg); // 输出异常信息 
        throw error
        //return Observable.throw(errMsg);
    }

}

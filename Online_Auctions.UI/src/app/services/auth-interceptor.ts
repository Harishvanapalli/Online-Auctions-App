import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuthenticationServiceService } from "./authentication-service.service";
import { Observable } from "rxjs";

@Injectable()

export class AuthInterceptorService implements HttpInterceptor{
    constructor(private authService : AuthenticationServiceService){}
    intercept(req: HttpRequest<any>, next: HttpHandler) {
        const token = this.authService.getToken();
        if(token){
            req = req.clone({
                setHeaders : {Authorization : `Bearer ${token}`}
            })
        }
        return next.handle(req);
    }
}
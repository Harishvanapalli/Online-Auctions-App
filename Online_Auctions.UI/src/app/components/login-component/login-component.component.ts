import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationServiceService } from 'src/app/services/authentication-service.service';
import { UserDetailsService } from 'src/app/services/user-details.service';

@Component({
  selector: 'app-login-component',
  templateUrl: './login-component.component.html',
  styleUrls: ['./login-component.component.css']
})
export class LoginComponentComponent {

  loginForm! : FormGroup;

  constructor(private fb: FormBuilder, private AuthenticationService : AuthenticationServiceService,
     private userDetails : UserDetailsService, private router : Router
  ){}

  ngOnInit() : void{
    this.loginForm = this.fb.group({
      emailID : ['', [Validators.required, Validators.email]],
      password : ['', Validators.required]
    })
  }

  onLogin(){
    if(this.loginForm.valid){
      this.AuthenticationService.CheckDetails(this.loginForm.value).subscribe({
        next : (res:any)=>{
            this.loginForm.reset();
            localStorage.setItem('token', res.token)
            localStorage.setItem('username', res.userName)
    
            this.userDetails.SetStatusToTrue();
            const payload = this.AuthenticationService.DecodeToken();

            this.userDetails.SetUserName(res.userName)
            this.userDetails.setUserActiveStatus(payload.IsSuspended);
            this.userDetails.SetUserRole(payload.role);
            this.userDetails.setUserEmail(payload.email);
            
            this.router.navigate(['/'])
            window.alert('Login Success')
        }, error: (err)=>{
          if (err.status === 401) {
            window.alert("Unauthorized: Incorrect login details.");
          } else if (err?.error?.message) {
            window.alert(err.error.message);
          } else {
            window.alert("An unexpected error occurred.");
          }
        }
      })
    }
  }

}

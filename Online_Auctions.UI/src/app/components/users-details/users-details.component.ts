import { Component } from '@angular/core';
import { UserModel } from 'src/app/models/User';
import { AdminActionsService } from 'src/app/services/admin-actions.service';

@Component({
  selector: 'app-users-details',
  templateUrl: './users-details.component.html',
  styleUrls: ['./users-details.component.css']
})
export class UsersDetailsComponent {

  constructor(private adminServices : AdminActionsService){}

  public Users : UserModel[] = [];

  ngOnInit(): void{
    this.adminServices.getAllUsersDetails().subscribe({
      next: (res: any)=>{
          if(res.isSuccess){
            this.Users = res.result;
          }else{
            console.log(res.message)
          }
      }, error : (err) => {
        if(err?.error?.message){
          console.log(err.error.message);
        }
      }
    })
  }
   
  SuspendToggle(user: UserModel) {
    const message = user.suspend ? "Are you sure to Resume the User" : "Are you sure to Suspend the User";
    if (window.confirm(message)) {
      user.suspend = !user.suspend;
      this.adminServices.UpdateUser(user).subscribe({
        next: (res: any) => {
          window.alert(res.message);
        },
        error: (err) => {
          window.alert(err?.error?.message || "An unexpected error occurred");
        }
      });
    }
  }  

}

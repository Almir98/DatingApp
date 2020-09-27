import { templateJitUrl } from '@angular/compiler';
import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  @ViewChild('editForm') editForm:NgForm;
  @HostListener('window:beforunload',['$event'])
  unloadNotification($event:any)
  {
    if(this.editForm.dirty)
    {
      $event.returnValue=true;
    }
  }

  user:User;

  constructor(private route:ActivatedRoute,private alertify:AlertifyService,private authService:AuthService,private userService:UserService) { }

  ngOnInit()
  {
    this.route.data.subscribe(data =>{
      this.user=data['users'];
    })
  }

  updateUser()
  {
    this.userService.update(this.authService.decodedToken.nameid,this.user).subscribe(next=>{

      console.log(this.authService.decodedToken.nameid);

      this.editForm.reset(this.user);
      this.alertify.success("Profile updated successfully");

    },error=>{
      this.alertify.error(error);
    });

  }


}

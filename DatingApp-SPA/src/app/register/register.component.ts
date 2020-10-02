import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model:any={};

  registerForm: FormGroup;

 @Output() cancelRegister=new EventEmitter(); 

  constructor(private authService:AuthService,private alertify:AlertifyService,private fb: FormBuilder) { }

  ngOnInit() {

    this.createRegisterForm();
  }

  createRegisterForm()
  {
    this.registerForm=this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['',Validators.required],
      dateOfBirth: [null,Validators.required],
      city: ['',Validators.required],
      country: ['',Validators.required],
      password: ['',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
      confirmPassword: ['',Validators.required]
    },{validator: this.passwordValidator})
  }

  passwordValidator(g: FormGroup)
  {
    return g.get('password').value === g.get('confirmPassword').value ? null : {'mismatch': true}
  }

   register()
   {
     console.log(this.registerForm.value);
  //   this.authService.register(this.model).subscribe(()=>{

  //     this.alertify.success('Registration successful');

  //   },error=>{
  //     this.alertify.error(error);
  //   });
   }

  cancel()
  {
    this.cancelRegister.emit(false);
  }


}

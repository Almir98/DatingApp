import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl=environment.apiUrl;

constructor(private http:HttpClient) { }


getUsers():Observable<User[]>
{
  return this.http.get<User[]>(this.baseUrl+'users');
}

getUser(id):Observable<User>
{
  console.log(id);
  return this.http.get<User>(this.baseUrl+'users/'+id);
}

update(id:number,user:User)
{
  return this.http.put(this.baseUrl+'users/'+id,user);
}

}

import { Observable, of } from 'rxjs';
import { User } from '../dtos/user';

export class MockUserService {
  getUsers(): Observable<User[]> {
    return of([{ id: 1, firstname: 'Mock User', lastname: '2', email: 'some@email.com' }]); // Simulated API response
  }
}

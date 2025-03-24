import { of } from 'rxjs';

export class MockUserService {
  getUsers() {
    return of([{ id: 1, name: 'Mock User' }]); // Simulated API response
  }
}

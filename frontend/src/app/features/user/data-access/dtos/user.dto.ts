export interface UserDto {
  id?: number;
  firstName: string;
  lastName: string;
  email: string;
}

export interface TotalCountDto {
  count: number;
}

export interface GroupUserCountDto {
  groupName: string;
  userCount: number;
}

import { UserDto } from '../dtos/user.dto';
import { User } from '../dtos/user';
import { Mapper } from '../../../../shared/data-access/mapper/mapper.interface';

export class UserMapper implements Mapper<UserDto, User>{
  public mapToModel(dto: UserDto): User {
    return {
      id: dto.id,
      firstname: dto.firstName,
      lastname: dto.lastName,
      email: dto.email,
    };
  }

  public mapToModels(dtos: Array<UserDto>): Array<User> {
    return dtos.map(value => this.mapToModel(value));
  }

  public mapToDto(model: User): UserDto {
    return {
      id: model.id,
      firstName: model.firstname,
      lastName: model.lastname,
      email: model.email,
    };
  }

  public mapToDtos(models: Array<User>): Array<UserDto> {
    return models.map(value => this.mapToDto(value));
  }
}

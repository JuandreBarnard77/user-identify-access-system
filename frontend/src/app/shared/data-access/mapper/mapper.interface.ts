export interface Mapper<T, U> {
  mapToModel?(dto: T): U;
  mapToModels?(dtos: Array<T>): Array<U>;
  mapToDto?(model: U): T;
  mapToDtos?(models: Array<U>): Array<T>;
}

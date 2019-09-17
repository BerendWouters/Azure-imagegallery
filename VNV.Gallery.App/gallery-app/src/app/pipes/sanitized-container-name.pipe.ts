import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'sanitizedContainerName'
})
export class SanitizedContainerNamePipe implements PipeTransform {

  transform(value: string): string {
    return value.replace('gallery-', '');
  }

}

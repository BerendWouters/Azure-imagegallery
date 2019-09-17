import { SanitizedContainerNamePipe } from './sanitized-container-name.pipe';

describe('SanitizedContainerNamePipe', () => {
  it('create an instance', () => {
    const pipe = new SanitizedContainerNamePipe();
    expect(pipe).toBeTruthy();
  });
});

import { memory } from './memory';

export class timeLine {
  id: number;
  name: string;
  description: string;
  ownerId: string;
  lastModified: string;
  memories: memory[];
}

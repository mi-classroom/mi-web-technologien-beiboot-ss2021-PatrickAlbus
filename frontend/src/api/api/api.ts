export * from './directory.service';
import { DirectoryService } from './directory.service';
export * from './file.service';
import { FileService } from './file.service';
export * from './startup.service';
import { StartupService } from './startup.service';
export const APIS = [DirectoryService, FileService, StartupService];
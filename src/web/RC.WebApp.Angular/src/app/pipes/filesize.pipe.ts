import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'filesize'
})
export class FileSizePipe implements PipeTransform {

    transform(size: number) {
        let calculatedSize = (size / (1024 * 1024));
        let extension = " MB";

        if (calculatedSize > 1024) {
            calculatedSize = (calculatedSize / 1024);
            extension = " GB";
        }

        return calculatedSize.toFixed(2) + extension;
    }

}
export class StringUtils {

    public static nullIfEmpty(val: string): string {
        if (val === undefined || val === null || val.trim() === '') {
            return null;
        }
        return val;
    };

    public static isNullOrEmpty(val: string): boolean {
        if (val === undefined || val === null || val.trim() === '') {
            return true;
        }
        return false;
    };
}


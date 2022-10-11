export class LocalStorageUtils {

    public getUser() {
        return JSON.parse(localStorage.getItem('rent-computer.user'));
    }

    public removeUserToken() {
        localStorage.removeItem('rent-computer.token');
    }

    public getUserToken(): string {
        return localStorage.getItem('rent-computer.token');
    }

    public setUserToken(token: string) {
        localStorage.setItem('rent-computer.token', token);
    }
}
export class LocalStorageUtils {

    public getUser() {
        // Simulando um usuário com permissões
        return {
            claims: [
                {
                    type: "Product",
                    value: "Read, Write"
                }
            ]
        }
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
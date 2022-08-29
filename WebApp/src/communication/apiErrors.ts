export default class StatusCodeNotOkError extends Error {
    res: Response;
    constructor(res: Response) {
        const message = res.status + res.statusText;
        super(message);
        this.res = res;
        // Set the prototype explicitly.
        Object.setPrototypeOf(this, StatusCodeNotOkError.prototype);
    }

    errorMessage() {
        return this.message;
    }
}
export interface IRepresentative {
    fullName: string;
    firstName: string;
    image: string
}

export class Representative implements IRepresentative {
    fullName: string;
    firstName: string;
    image: string;

    constructor(name: string, image: string) {
        this.fullName = name
        this.image = image
    }
}

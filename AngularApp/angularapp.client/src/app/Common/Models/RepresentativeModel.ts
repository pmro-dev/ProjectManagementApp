export interface IRepresentativeModel {
    fullName: string;
    firstName: string;
    image: string
}

export class RepresentativeModel implements IRepresentativeModel {
    fullName: string;
    firstName: string;
    image: string;

    constructor(name: string, image: string) {
        this.fullName = name
        this.image = image
    }
}
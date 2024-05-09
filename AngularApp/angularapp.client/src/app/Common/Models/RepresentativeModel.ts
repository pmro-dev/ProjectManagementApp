export interface IRepresentativeModel {
    name: string;
    image: string
}

export class RepresentativeModel implements IRepresentativeModel {
    name: string;
    image: string;

    constructor(name: string, image: string) {
        this.name = name
        this.image = image
    }
}
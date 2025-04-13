export interface ITag {
    id: number;
    title: string;
}

export class Tag implements ITag {
    id: number;
    title: string;

    constructor(id: number, title: string) {
        this.id = id
        this.title = title
    }

    public static createTagModel(tagSource: ITag) : ITag{
        return new Tag(tagSource.id, tagSource.title);
    }
}

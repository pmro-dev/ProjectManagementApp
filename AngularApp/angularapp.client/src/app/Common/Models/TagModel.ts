export interface ITagModel {
    id: number;
    title: string;
}

export class TagModel implements ITagModel {
    id: number;
    title: string;

    constructor(id: number, title: string) {
        this.id = id
        this.title = title
    }

    public static createTagModel(tagSource: ITagModel) : ITagModel{
        return new TagModel(tagSource.id, tagSource.title);
    }      
}
export class Recipe {
    public name: string;
    public description: string;
    public imagepath: string;

    constructor(objName: string, objDesc: string, objImagePath: string) {
        this.name = objName;
        this.description = objDesc;
        this.imagepath = objImagePath;
    }

}

import { StudyField } from "../enums/studyField.enum";
import { StudyProgram } from "../enums/studyProgram.enum";

export class DegreeDto {
    name!: string;
    location!: string;
    startYear!: Date;
    endYear!: Date;
    studyProgram!: StudyProgram;
    studyField!: StudyField;
}
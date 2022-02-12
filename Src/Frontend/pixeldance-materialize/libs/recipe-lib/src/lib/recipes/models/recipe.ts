import { Ingredient } from "./ingredient";
import { PriorityLevel } from "./priority-level";

//
export interface Recipe {
    id: string;
    slug: string;
    title: string;
    img: string;
    imgFile: File;
    category?: string;
    categoryId: string;

    preparation: string;
    description: string;

    liked: boolean;
    position: number;
    priority: PriorityLevel;
    
    ingredients: Ingredient[];
    
}

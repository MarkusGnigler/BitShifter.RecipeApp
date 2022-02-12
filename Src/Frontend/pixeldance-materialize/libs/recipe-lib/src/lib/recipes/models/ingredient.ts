import { PriorityLevel } from "./priority-level";

export interface Ingredient {
    title: string;
    quantity: number;
    unit: string;
    priority: PriorityLevel;
}
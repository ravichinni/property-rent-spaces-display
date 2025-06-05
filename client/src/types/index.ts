export interface Property {
    id: string;
    name: string;
    description: string;
    features: string[];
    highlights: string[];
    transportation: Transportation[];
    spaces: Space[];
}

export interface Transportation {
    name: string;
    distance: string;
}

export interface Space {
    id: string;
    name: string;
    description: string;
    rentRoll: RentRoll[];
}

export interface RentRoll {
    month: string;
    amount: number;
} 
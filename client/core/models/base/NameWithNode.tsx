import React from "react";
import { BaseProps } from "./BaseProps";

export interface NameWithNode extends BaseProps {
    name: string;
}

export interface RouterWithElement extends NameWithNode {
    route: string,
    onClick: () => void,
    children: React.ReactElement,
}
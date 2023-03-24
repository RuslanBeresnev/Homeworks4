namespace LambdaInterpreter

open System

type Term =
    | Variable of char
    | Abstraction of char * Term
    | Application of Term * Term
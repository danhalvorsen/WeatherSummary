// import { render, screen } from '@testing-library/react';
 //import axios from "axios";





function supercomplexAdd(a: () => number, b: () => number): number {
    return a() + b();
}

test('My API Test', () => {
    const myMockA = jest.fn();
    myMockA.mockReturnValueOnce(10.0);
    const myMockB = jest.fn();
    myMockB.mockReturnValueOnce(11.0);
    expect(supercomplexAdd(myMockA, myMockB)).toBe(21);
});




import EasyAdd from './Easyadd';
import Myvaluefunction from './Myvaluefunction'


// function easyAdd(a: () => number, b: () => number): number {
//     return a() + b();
// }

test('My easy Test', () => {

 
    const myA = (aa=19):number=> aa+1; 
    const myB = (bb=29):number=> bb+1; 

    expect(EasyAdd(myA, myB)).toBe(50);
});





// type User = {
//     id: number,
//     city: string,
//     date: Date
// }

// type GetUserResponse = {
//     data: User[];
// }


    // test('My first love with unit testing', () => {
// //     expect(add(55, 45)).toBe(100);
// // });
// function add(a: number, b: number): number {
//     return a + b;
// }
// function supercomplexAdd(a: () => number, b: () => number): number {
//     return a() + b();
// }
// // test('My API Test', () => {
// //     const myMockA = jest.fn();
// //     myMockA.mockReturnValueOnce(10.0);
// //     const myMockB = jest.fn();
// //     myMockB.mockReturnValueOnce(11.0);
// //     expect(supercomplexAdd(myMockA, myMockB)).toBe(21);
// // });
// type User = {
//     id: number,
//     city: string,
//     date: Date
// }
// type GetUserResponse = {
//     data: User[];
// }

import sum from './sum';

test('My first love with unit testing', () => {
    expect(add(55, 45)).toBe(100);
});

function add(a: number, b: number): number {
    return a + b;
    
};



















export{}


// type t = Array<number> | undefined | null;

// export async function foo(): Promise<t> {
    
//     let final = new Array<number>();

//     try {
//         const result = await (await axios.get<Array<number>>('http://localhost:3000/source')).data;
//         return result;

//     }
//     catch (err) {
//         console.log(err);
//     }
// }

// test('Testing the APIProcessor', () => {

//     expect(ApiProcssor(foo)).toBe(100)
// });






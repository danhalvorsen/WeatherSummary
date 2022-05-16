export default 

function EasyAdd(a: () => number, b: () => number): number {
    return a() + b();
}


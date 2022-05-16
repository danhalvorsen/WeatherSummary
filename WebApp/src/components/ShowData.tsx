import React , {useEffect , useState} from "react"
import axios from "axios";

function ShowData(): any {
    return 0;
}

// function ShowData(): any {

//     type User= {
//         id: number,
//         city: string,
//         date: Date
//     }
//     type GetUserResponse = {
//         data: User[];
//     }
   
//     // interface Idata {
//     //     res: string,
//     //     city: string
//     // }

//     const [data, setData] = useState({});

    
// //     useEffect(()=>{
// //         axios.get<GetUserResponse>(`https://localhost:5001/2022-05`)
// //         .then(res => {
// //         setData(res);
// //         console.log(data);
// //         }
// //         )

// // // return ()=> {
// // //    // console.log(data);
// // // }
// //         //return ()=>{ console.log(data)}
// //        // console.log(data);
// //     },[])

//     console.log(data);
//    // const html = JSON.stringify(data);
//    // console.log(data);


   
//     return(
//     <div>
//         The city is: {data}
//     </div>
//     )
// }


export default ShowData;

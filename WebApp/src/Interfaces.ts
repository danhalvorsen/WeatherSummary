export interface IresultJson {
    city: string
    date: string
    weatherType: string
    temperature: number
    windspeed: number
    windDirection: number
    windspeedGust: number
    pressure: number
    humidity: number
    probOfRain: number
    amountRain: number
    cloudAreaFraction: number
    fogAreaFraction: number
    probOfThunder: number
    source: {
        dataProvider: string
    }
}

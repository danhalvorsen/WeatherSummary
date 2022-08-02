<App>
    <WeatherForcastSearchState>
        <WeatherForcastSearch>
            <InputCity></InputCity><SearchButton></SearchButton>
            <SelectSearchOptionState>
                <SelectSearchOption>
                    <DayPicker></DayPicker>
                </SelectSearchOption>
            </SelectSearchOptionState>

            <SelectSearchOptionState>
                <SelectSearchOption>
                    <WeekPick></WeekPick>
                </SelectSearchOption>
            </SelectSearchOptionState>
            <SelectSearchOptionState>
            </WeatherForcastSearchState>

            <SelectSearchOptionState>
                <SelectSearchOption>
                    <RadioButton>
                        <FromDate></FromDate>
                        <ToDate></ToDate>
                    </RadioButton>
                </SelectSearchOption>
            </SelectSearchOptionState>
        </WeatherForcastSearch>
        <ListState>
            <List>
                <ListItem>

                </ListItem>
            </List>
        </ListState>
    </WeatherForcastSearchState>


    abstract class ListItem {
    }

    class WeatherProvicerListItem extends ListItem{

    }

    class CarListItem extends ListItem{

    }









</App>
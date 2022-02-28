# Project_ST2TRD_SOUKSAMLANE_LEXTRAIT
 
 
## Authors 
**LEXTRAIT** Hugo  
**SOUKSAMLANE** Hugo

## Abstract

**Altinium allows the user to analyse unavoidable informations about the top 10 cryptocurencies in 2022.** The application allows you to navigate between different pages of information related to a crypto currency. It allows you to analyze the price graphs and some information such as its trend over the past days

## Application's purpose

Altinium is a C#-coded with WPF graphical interface application which displays datas linked to the 10-most important current cryptocurrencies.
Our application calculates 365-days total volume, 1/7/30 days prices' trend, correlation between 2 different cryptocurrencies during desired time period, market capitalization, etc...
We designed the WPF application the more userfriendly possible in order to display more intuitive and fancier information such as cryptocurrencies closing prices' graphs evolution through time, green/red bearish/bullish button depending on each evolution, interactive correlation calculator feature, etc...

## How does it work?
It retrieves some datas linked to the cryptocurrency world by using CryptoCompare API and its requests.
First of all, CryptoCompare's website generate an appropriate URL in order to request some datas to the API.
Then, thanks to the request, we receive a json file which contains all the wanted datas in objects forms.
By using deserialization process, we converts the .json file into Crypto objects which has the exact same structure.
Finally, with these internal C# Crypto object, we were able to manipulate it by creating dictionaries, lists, arrays, etc..

## Features availables
The list below represents an exhaustive list of the different functionalities that the user can do within the application.

- View top 10 cryptocurrencies by market Capitalization
- Analyze trend 3 periods availables {24 hours, 14 days, 30 days}
- Total volume in dollar echanged last 6 months
- Get specific informatiosn on a crytocurrency
- Scatter graph of the closing price of the choosen asset
- Add optionnal asset's graph to compare to the chosen cryptocurrency
- Move graph and zoom on specific period
- Correlation analysis to another asset on a user choosen period
- Daily market capitalization
- Navigate easily between application's pages 

## Functions 
- DatetTime datetimeConvertor(string date) : takes a string format date and returns the same date with a datetime format
- string growthCalculator(Crypto crypto_input, int number_days) : initializes a dictionary containg the closing prices of the Cryptocurrency given in parameter. On the other hand, following the second parameter "number_days", it creates today's datetime and an other datetime which corresponds to today's date minus the number_days. Finally, it calculates the evolution of the cryptocurrency's closing price from this past datetime and today, and return "Bearish", "Neutral" or "Bullish" depending on the result's sign.
- string FormatDoubleValues(double valueLength, double dictValue) : this function basically converts an input double value into a string value. In addition, it changes the string format depending on the number of thousands (1K for 1 000, 1M for 1 000 000, 1B for 1 000 000 000)
- string totalVolumeCalculator (Crypto crypto_ticker) : calcultes a specific cryptocurrency's total volume in a given period of time.
- IDictionary <string,double> dictionaryGenerator (Crypto crypto_ticker, string column, int number_datas) : generates and returns a dictionary containing specific cryptocurrency column data (Open, Close, High, Low, Volume) as Value, and the corresponding date as KeyValye. The number of retrieved data is set by the number_datas parameter. In addition, this function has been overloaded in order to sometimes return a <string,string> dictionary, a <Datetime,double> one or even a <string, double> one.
- double [] dictValuesToArray(IDictionary<string,double> closingValues) : creates an array which contains all the Values of a given dictionary.
- string correlationCalculator (Crypto firstCrypto, Crypto secondCrypto, int n) : performs and returns the correlation coefficient between two cryptocurrencies. It firstly calls the dictionaryGenerator twice in order to create two dictionary containing each cryptocurrency closing prices. Then, it also calls dictValuesToArray twice in order to create two arrays containing the two dictionaries values (and get rid of the date in the KeyValues). Finally, with these two parallel arrays, we execute the whole correlation calculation.
- exitApp(object sender, RoutedEventArgs e) : quits the application
- void toggleTheme(object sender, RoutedEventArgs e) : toggle theme between dark and light
- void goToXXX(object sender, RoutedEventArgs e) : initializes a page XX and switch the current page to the new generated page
- updateTrendElement(Card cardTrend, TextBlock textBlockTrend, string trend) : get the color corresponding to the trend
- setData() : major function that initialize for each cryptocurrency when the landing page loads and call functions to updates values.
- backHomePage(object sender, RoutedEventArgs e) : navigates back to the landingPage
- void addToScatter(Crypto asset, ScottPlot.WpfPlot plot, System.Drawing.Color color) : adds a scatter of a specific crypto's closing price to targeted Scottplot graph
- void refreshGraph(Crypto asset1, Crypto asset2 = null, Crypto asset3 = null, Crypto asset4 = null) : Add multiple scatter to graph with specific colors
- showXXX(object sender, RoutedEventArgs e) : turns to true variable to show a specific scatter graph. Allow us to enable a graph or not
- void setmarketCap() : get and set the Daily market cap for a specific asset
- void corrSelectionChanged(object sender, SelectionChangedEventArgs e) : Determines the correlation between 2 assets according to the user selection.
- void PreviewTextInput(object sender, TextCompositionEventArgs e) : Allow us to manage the user input on a TextBox

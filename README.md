# Project_ST2TRD_SOUKSAMLANE_LEXTRAIT
 
 
## Authors 
**LEXTRAIT** Hugo  
**SOUKSAMLANE** Hugo

## Abstract

**Altinium allows the user to analyse unavoidable informations about the top 10 cryptocurencies in 2022.** The application allows you to navigate between different pages of information related to a crypto currency. It allows you to analyze the price graphs and some information such as its trend over the past days

## Application's purpose

Altinium is a C#-coded WPF application which displays datas linked to the 10-most important current cryptocurrencies.
Our application calculates 365-days total volume, 1/7/30 days prices' trend, correlation between 2 different cryptocurrencies during desired time period, market capitalization, etc...
We designed the WPF application in order to display more intuitive and fancier information such as cryptocurrencies closing prices' graphs evolution through time, green/red bearish/bullish button depending on each evolution, interactive correlation calculator feature, etc...

## How does it work?
It retrieves some datas linked to the cryptocurrency world by using CryptoCompare API and its requests.
First of all, CryptoCompare's website generate an appropriate URL in order to request some datas to the API.
Then, thanks to the request, we receive a json file which contains all the wanted datas in objects forms.
By using deserialization process, we converts the .json file into Crypto objects which has the exact same structure.
Finally, with these internal C# Crypto object, we were able to manipulate it by creating dictionaries, lists, arrays, etc..

## Which function

## Features availables

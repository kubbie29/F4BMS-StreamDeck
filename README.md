# Falcon 4 BMS SharedMem Plugin for Stream Deck

A Streamdeck Plugin that will display information from Falcon 4 BMS's Shared Memory resources.

## Installation Instructions
* Download the .streamDeckPlugin and run it to install into your StreamDeck.

## Current Features (Version 0.2.0)

* ### Engine Gauges (Added with 0.2.0)
	* Display the following gauges:
		* RPM Percentage
		* FTIT
		* Nozzle Position
		* Oil Pressure
	* Set the decimal format selected gauge (all examples will use *FTIT: 0.01924542346*):
		* Raw format - No formatting, no limit on decimals displayed. Ex. FTIT: 0.01924542346
		* One Decimal - Round the raw data with only One decimal place. Ex. FTIT: 0.0
		* Two Decimals - Round the raw data to Two decimal places. Ex. FTIT: 0.02
		* Whole Number - Round the raw data to whole numbers with no decimals. Ex. FTIT: 0
		
* ### CMDS Panel
	* Display Flare and Chaff Counts.
	* Display Current CMDS Mode (OFF/STBY/MAN/SEMI/AUTO/BYP).
	* Display Flare and Chaff Low Warning Indicators.
	* Display GO/NOGO/DISPENSE RDY Indicators.

>[!NOTE]
> Updated with Version 0.2.0
>
> Only one custom text area available now. This will set the custom Indicator text for the selected Indicator.
> When first selected the text area will be blank but each Indicator will have the following default text if nothing is entered.
> * Chaff Low - CHAFF LO
> * Flare Low - FLARE LO
> * GO - GO
> * NOGO - NOGO
> * DISPENSE RDY - DISPENSE RDY

All Display formatting is done with the default StreamDeck Title formatting options. 

> [!WARNING]
> Any text placed in the Title text box will override the plugin information, so it must be blank to work.

![Title Format Options](./titleformatoption.png)

## Useful Things
Icons that can be used for the buttons located under the KeyIcons folder

![Flare Icon](./KeyIcons/flareIcon.png)
![Chaff Icon](./KeyIcons/chaffIcon.png)
![Mode Icon](./KeyIcons/modeIcon.png)
![Oil Pressure](./KeyIcons/oil.png)
![FTIT](./KeyIcons/ftit.png)
![Nozzle Position](./KeyIcons/nozpos.png)
![RPM %](./KeyIcons/rpm.png)

## TODO
* Only show the Indicator text areas when those options are selected.
* Add default indicator text to the Indicator text areas.

## Future Roadmap
* Ability to use the buttons as hotkeys to trigger BMS keybinds. EX: add the keybind to the CMDS Mode button to cycle the mode in BMS

## Helpful Tools
* [StreamDeck-Joy](https://github.com/ashupp/Streamdeck-vJoy) - To bind vJoy buttons to bind DX keybinds in BMS. So you can use the StreamDeck to toggle switches.


## Dependencies
* [BarRaider's StreamDeck-Tools](https://github.com/BarRaider/streamdeck-tools)
* F4SharedMem library from [Lightning Viper's Lightning Tools](https://github.com/lightningviper/lightningstools)




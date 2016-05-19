# DS3ParamExtractor

This is a tool to extract binary .param and .fmg files of Dark Souls III.

Binaries can be found under [releases](https://github.com/Pireax/DS3ParamExtractor/releases).

To use this tool extract the .param files from Data0.bdt using something like [BinderTool](https://github.com/Atvaark/BinderTool).
Then simply place this in the same folder as the .param files and then run the tool. By default the tool outputs in csv format.
You can also supply a file path to DarkSouls3ExtractionTool to export that.

**Arguments**
* `-h`  Help.
* `-H`  Output column headers.
* `-H+` Output structure offsets and headers.
* `-d`  Debug Mode, outputs hidden columns.
* `-g`  Generate .proto file.
* `-p`  Output in protobuf format.

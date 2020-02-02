# Command Line usage

For the tool to work in the work folder there must be a bookgen.json config file.
This config file can be created with the following command:

```
BookGen Build -a Init
```

To get information regarding the configuration file bookgen.json file run:
```
Bookgen ConfigHelp
```

## GUI Usage
```
    BookGen Gui {-v} {-d [directory]}
    BookGen Gui {--verbose} {--dir [directory]}

    Arguments:
    -d, --dir:
        Optional argument. Specifies work directory. If not specified, then
        the current directory will be used as working directory.
    -v, --verbose: 
        Optional argument, turns on detailed logging. Usefull for locating issues
```

## Update Usage
```
    BookGen Update {-p} {-s}
    BookGen Update {--prerelease} {--searchonly}

    Arguments:
      -s, --searchonly:
        Optional argument. Search and display latest release,
        but don't actually do an update
     -p, --prerelease:
        Include pre relase versions.
```

## Single File rendering usage
```
    BookGen Md2HTML -i [input.md] -o [output.html] {-c [cssfile.css]}
    BookGen Md2HTML --input [input.md] --output [output.html] {--css [cssfile.css]}

    Arguments:
    -i, --input: 
        Input markdown file path
    -o, --output: 
        Output html file path
    -c. --css:
        Optional argument. Specifies the css file to be aplied to the html
```

## Assembly documenter usage
```
    BookGen AssemblyDocument -a [assembly.dll] -x [assembly.xml] -o [output directory]
    BookGen AssemblyDocument --assembly [assembly.dll] -xml [assembly.xml] --output [output directory]

    Arguments:
    -a, --assembly:
        Input assembly file
    -x, --xml:
        Visual studio generated XML documentation of the Assembly
    -o, --output:
        Specifies the output directory. Each tipe will be written to a
        sepperate .md file

Note: 
    This feature is experimental. Results might not be complete.
    It is mainly used to generate the Script API documentation.
```

## Build Usage
```
    BookGen Build -a [action] {-v} {-d [directory]} {-n}
    BookGen Build --action [action] {--verbose} {--dir [directory]} {--nowait}

  Arguments:
    -a, --action: 
        Specifies the build action. See below.
    -d, --dir:
        Optional argument. Specifies work directory. If not specified, then
        the current directory will be used as working directory.
    -v, --verbose: 
        Optional argument, turns on detailed logging. Usefull for locating issues
    -n, --nowait:
        Optional argument, when specified & the program is fisihed,
        then it immediately exits, without key press.
```
### Build Actions:
* ```BuildEpub``` - Build Ebup 3.2
* ```BuildPrint``` - Build Printable HTML document
* ```BuildWeb``` - Build Releaseable Static Website
* ```BuildWordpress``` - Build Wordpress Export XML file
* ```Clean``` - Clean output folders
* ```Initialize``` - Initialize dir as BookGen project
* ```Test``` - Build Test Static Website & Run test server
* ```ValidateConfig``` - Validates the bookgen.json configuration file
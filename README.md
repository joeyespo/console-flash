ConsoleFlash
============

A simple Windows command-line app to flash the console window, letting you know
when a script finishes.

![Screenshot](Screenshots/Screenshot.png)


Examples
--------

Test it out in the command line:

```bat
> sleep 3s && flash
```

A more practical place is at the end of a lengthy `.bat` file where you'd like to
see the output when it finishes:

```bat
@ECHO OFF

initial-setup-code
some-really-long-executable

flash
pause
```


Usage
-----

    flash [options]

#### Options

    /d decimal          The number of seconds to wait before beginning to flash (default: 0)
    /c integer          The number of times to flash the window before keeping it inverted, with zero indicating do not stop (default: 3)
    /r decimal          The flash rate in seconds, with zero indicating use the system default (default: 0)
    /?, /h              Shows this help message

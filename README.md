ConsoleFlash
============

A simple Windows command-line app to flash the console window, letting you know
when a script finishes. A common place to use this at the end of a batch file.


Screenshots
-----------

![Console Flash: Screenshot 1](http://s3.amazonaws.com/scrnshots.com/screenshots/287362/console_flash_taskbar_screenshotpng)

An example of the console window flashing.


Usage
-----

    flash [options]


**Options**

    /d decimal          The number of seconds to wait before beginning to flash (default: 0)
    /c integer          The number of times to flash the window before keeping it inverted, with zero indicating do not stop (default: 3)
    /r decimal          The flash rate in seconds, with zero indicating use the system default (default: 0)
    /?, /h              Shows this help message

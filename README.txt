18-Aug-2021
Rick Rigby/github.com:FalkiD
UsbDeviceWakeup WCF service and client example apps.

This example is available under the MIT license. No guarrantees of any kind
are made. Use at your own risk. Your mileage may vary.

I ran into a nasty issue where legacy USB 1.1 devices are turned OFF
when inactive on Windows 10. This behavior is very different than
Windows 7. A working production test system for many existing products
was completely broken when moved to Win10.

This example shows a service EXE that can be run as an administrative
user at login on a production test system. The client app code can be
pasted into the existing production test program, which runs as an
ordinary user. Some magic keystroke can be added to the production
test program which calls the Wakeup function as needed. The function
signature can be changed to specify the VID if necessary.

This brought our production test program back to life on Win10.

There may be(probably are) security issues to worry about. Perhaps
deleting the tcp connection and only using the localhost named pipe
is better. Typically production test systems are not connected
to the global internet at all.

Your mileage may vary.

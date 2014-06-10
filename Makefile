all: Program.exe

Program.exe: Program.cs
	mkdir -p bin/Release
	mcs -debug- -out:bin/Release/$@ -pkg:dotnet -r:Mono.Posix.dll -r:ServiceStack.dll -r:ServiceStack.Interfaces.dll -r:ServiceStack.Razor.dll -r:ServiceStack.ProtoBuf.dll Program.cs
	cp -v *.dll bin/Release/

clean:
	rm -f bin/Release/*.exe bin/Release/*.dll

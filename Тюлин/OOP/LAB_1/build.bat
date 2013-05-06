set borladdir=D:borlandc

bcc -I%borladdir%\include;.\src -L%borladdir%\lib -nbin -elab.exe .\src\*.cpp

pause
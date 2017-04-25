#ifndef INTERFACEPUFFY_H
#define INTERFACEPUFFY_H
#include <QMainWindow>
#include "Headers/interfacepuffy.h"
#include "Headers/mytcpsocket.h"

// To create the Interface, permit to connect it with the InterfacePuffy.ui file
namespace Ui {
class InterfacePuffy;
}

class InterfacePuffy : public QMainWindow
{
Q_OBJECT

public:
    InterfacePuffy(QWidget *parent = 0);
    ~InterfacePuffy();

// The 3 buttons on the interface, connected on clic
public slots:
    void startPuffy();
    void introMode();
    void storyMode();
    void thirdMode();
    void stopPuffy();
    void quit();

private:
   void closeEvent(QCloseEvent *bar);
//permit to connect with the InterfacePuffy.ui file
   Ui::InterfacePuffy *ui;
// Tcp client to talk with the server in the C# code in puffy
   MyTcpSocket clientSocket;

};

#endif // INTERFACEPUFFY_H

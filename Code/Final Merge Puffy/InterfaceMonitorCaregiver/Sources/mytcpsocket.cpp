#include "Headers/mytcpsocket.h"
#include <QMessageBox>
#include <QString>
#include <QIcon>

MyTcpSocket::MyTcpSocket(QObject *parent) :
    QObject(parent)
{
}

void MyTcpSocket::doConnect()
{
    // Create the client Tcp
    socket = new QTcpSocket(this);
    // Connect the signal of the connection to the functions
    connect(socket, SIGNAL(connected()),this, SLOT(connected()));
    connect(socket, SIGNAL(disconnected()),this, SLOT(disconnected()));
    connect(socket, SIGNAL(bytesWritten(qint64)),this, SLOT(bytesWritten(qint64)));
    connect(socket, SIGNAL(readyRead()),this, SLOT(readyRead()));

    qDebug() << "connecting...";

    // this is not blocking call
    //(hostname, port, openmode, -)
    socket->connectToHost("192.168.1.79", 22); // change the IP config of the computer
}

void MyTcpSocket::connected()
{
    // Show message on Console
    qDebug() << "connected...";
}

void MyTcpSocket::disconnected()
{
    // Show message on Console
    qDebug() << "disconnected...";
}

void MyTcpSocket::writeCmd(int cmd){
    // Show the commande of the button on Console (0, 1 or 2)
    qDebug() << "write cmd " << cmd;
    // Write command on the Tcp connexion to the Server
    switch (cmd) {
    case 1:
        socket->write("1");
        break;
    case 2:
        socket->write("2");
        break;
    case 3:
        socket->write("3");
        break;
    case 4:
        socket->write("4");
        break;
    case 5:
        socket->write("5");
        break;
    default:
        socket->write("0");
        break;
    }
}

void MyTcpSocket::bytesWritten(qint64 bytes)
{
    //Show the bytes written to the Server (same as write cmd if good)
    qDebug() << bytes << " bytes written...";
}

void MyTcpSocket::readyRead()
{
    qDebug() << "reading...";
    // Read the data on the Tcp connexion sent by the server
    QByteArray readData = socket->readAll();
    qDebug() <<  readData;// read the data from the socket
    // Put the data readed in QString to print it in a box message
    QString data = readData.toStdString().c_str();
    // Show the box message with the data
    QMessageBox msg;
    msg.setText("Reading Data");
    msg.setInformativeText(data);
    msg.setIcon(QMessageBox::Information);
    msg.exec();
}

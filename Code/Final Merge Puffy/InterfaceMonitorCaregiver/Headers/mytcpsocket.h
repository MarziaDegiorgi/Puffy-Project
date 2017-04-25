#ifndef MYTCPSOCKET_H
#define MYTCPSOCKET_H

#include <QObject>
#include <QTcpSocket>
#include <QDebug>

// Class which is the Tcp client to talk with the Tcp Server in C# in Puffy
class MyTcpSocket : public QObject
{
    Q_OBJECT
public:
    explicit MyTcpSocket(QObject *parent = 0);
// Connect to the server by Ip adress
    void doConnect();
// Write command to the Tcp Server
    void writeCmd(int);

signals:
// Functions connected to the Tcp connexion signals, do as the name say
public slots:
    void connected();
    void disconnected();
    void bytesWritten(qint64 bytes);
    void readyRead();


private:
// Create the object Tcp (client)
    QTcpSocket *socket;
};

#endif // MYTCPSOCKET_H

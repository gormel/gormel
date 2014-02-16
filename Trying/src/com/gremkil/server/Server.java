package com.gremkil.server;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

public class Server {

    private static Server inst = null;
    public static Server getInstance() {
        if (inst == null) {
            inst = new Server();
        }
        return inst;
    }

    private ServerSocket serverSocket;
    private Thread serverThread;
    private List<Client> clients = new ArrayList<Client>();

    private class Client extends Thread {
        private Socket clientSocket;
        private Server server;
        private InputStream socketInputStream;
        private OutputStream socketOutputStream;

        private Client(Socket clientSocket, Server server) {
            this.clientSocket = clientSocket;
            this.server = server;
            try {
                socketInputStream = clientSocket.getInputStream();
                socketOutputStream = clientSocket.getOutputStream();
            } catch (IOException e) {
                throw new RuntimeException(e);
            }
        }

        @Override
        public void run() {
            throw new UnsupportedOperationException();
        }

        public void send() {
            throw new UnsupportedOperationException();
        }
    }

    public void start(int port) {
        try {
            serverSocket = new ServerSocket(5555);
            serverThread = new Thread() {
                @Override
                public void run() {
                    while (true) {
                        try {
                            Socket client = serverSocket.accept();
                            Client c = new Client(client, Server.this);
                            clients.add(c);
                            c.start();
                        } catch (IOException e) {
                            throw new RuntimeException(e);
                        }
                    }
                }
            };
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    private void onPackageRecived(Client c) {
        throw new UnsupportedOperationException();
    }

    public void stop() {
        serverThread.interrupt();
    }
}

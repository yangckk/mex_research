
CC=g++
LDFLAGS=-L/usr/local/lib `pkg-config --libs protobuf grpc++ grpc` -Wl,--no-as-needed -lgrpc++_reflection -Wl,--as-needed -ldl 

UnityServer: UnityServer.o road.pb.o road.grpc.pb.o PID.o
	$(CC) UnityServer.cc road.pb.cc road.grpc.pb.cc PID.cc $(LDFLAGS) -g -o  UnityServer  

clean:
	rm -rf UnityServer.o road.pb.o road.grpc.pb.o PID.o UnityServer

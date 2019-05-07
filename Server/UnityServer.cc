/*
  Authors:
  Bill Patterson
  Jeffrey Tsou
 */

#include <google/protobuf/util/time_util.h>


#include <grpcpp/grpcpp.h>
#include "road.grpc.pb.h"
using grpc::Server;
using grpc::ServerBuilder;
using grpc::ServerContext;
using grpc::Status;

#include <string>
#include <queue>
#include <chrono>

using namespace std;

queue<long> dTimes;
long total = 0;
class PositionServiceImpl final : public Position::Service {
  Status SendPosition(ServerContext* context, const PositionRequest* request,
		      PositionReply* reply) override {
    if(request->x() < 0)
      {
	reply->set_pos("Left");
	reply->set_actuationforce(1.0);
      }
    else
      {
	reply->set_pos("Right");
	reply->set_actuationforce(-1.0);
      }
       long nanoseconds_since_epoch =  std::chrono::duration_cast<std::chrono::nanoseconds> (std::chrono::system_clock::now().time_since_epoch()).count();
       google::protobuf::Timestamp serverUTC = google::protobuf::util::TimeUtil::NanosecondsToTimestamp(nanoseconds_since_epoch);
       google::protobuf::Timestamp* p_serverUTC = &serverUTC;
       reply->PositionReply::set_servertime(google::protobuf::util::TimeUtil::ToString(serverUTC));
       //reply->set_dtime(google::protobuf::util::TimeUtil::DurationToNanoseconds(serverUTC - request->PositionRequest::clienttime()));
       long currentDTime = google::protobuf::util::TimeUtil::DurationToNanoseconds(serverUTC - request->PositionRequest::clienttime());
       dTimes.push(currentDTime);
       total += currentDTime;
       if(dTimes.size() > 20)
	 {
	   total -= dTimes.front();
	   dTimes.pop();
	 }
       reply->set_dtime(total/dTimes.size());
       
       
    return Status::OK;
  }
};

void RunServer(string address) {
  string server_address(address);
  PositionServiceImpl service;

  ServerBuilder builder;

  builder.AddListeningPort(server_address, grpc::InsecureServerCredentials());
  builder.RegisterService(&service);
  unique_ptr<Server> server(builder.BuildAndStart());
  cout << "Server listening on " << server_address << endl;
  server->Wait();
}

int main(int argc, char** argv) {
  if (argc > 0) {
    RunServer(argv[1]);
  }
  else
    {
      cout << "Must pass IP Address of server as argument" << endl;
      return 1;
    }
  return 0; 
}

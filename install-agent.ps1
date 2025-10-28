$agentUrl = "https://vstsagentpackage.azureedge.net/agent/3.225.0/vsts-agent-win-x64-3.225.0.zip"
$agentDir = "C:\azagent"
$orgUrl = "https://dev.azure.com/MissonDevopsCertification"
$pat = "DC8AAChdx2DWLsSxi22LBZXIBNretHD0ahroMtPAwgk3qFeruN53JQQJ99BJACAAAAAAAAAAAAASAZDO27AX"
$pool = "MoodPulsePool"

# Create agent directory
New-Item -ItemType Directory -Force -Path $agentDir

# Download and extract agent
Invoke-WebRequest -Uri $agentUrl -OutFile "$agentDir\agent.zip"
Expand-Archive "$agentDir\agent.zip" -DestinationPath $agentDir

# Configure agent
cd $agentDir
.\config.cmd --unattended --url $orgUrl --auth pat --token $pat --pool $pool --acceptTeeEula

# Install and start agent as Windows service
.\svc install
.\svc start


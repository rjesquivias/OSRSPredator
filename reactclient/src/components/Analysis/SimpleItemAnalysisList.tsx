import { List } from 'semantic-ui-react';
import SimpleItemAnalysisSegment from './SimpleItemAnalysisSegment';

interface Props {
    simpleItemAnalysisList: any[]
}

export default function SimpleItemAnalysisList({simpleItemAnalysisList}: Props) {
    
    return (
        <List>
            {simpleItemAnalysisList && simpleItemAnalysisList.map((simpleItemAnalysis: any) => (
                <List.Item key={simpleItemAnalysis.id}>
                    <SimpleItemAnalysisSegment 
                        id={simpleItemAnalysis.itemDetails ? simpleItemAnalysis.itemDetails.id : "0"}
                        name={simpleItemAnalysis.itemDetails ? simpleItemAnalysis.itemDetails.name: 'null itemDetails'} 
                        delta={simpleItemAnalysis.delta} 
                        examine={simpleItemAnalysis.itemDetails ? simpleItemAnalysis.itemDetails.examine: 'null itemDetails'} 
                        high={simpleItemAnalysis.mostRecentSnapshot ? simpleItemAnalysis.mostRecentSnapshot.high : 0} 
                        highTime={simpleItemAnalysis.mostRecentSnapshot ? simpleItemAnalysis.mostRecentSnapshot.highTime: 0} 
                        low={simpleItemAnalysis.mostRecentSnapshot ? simpleItemAnalysis.mostRecentSnapshot.low : 0} 
                        lowTime={simpleItemAnalysis.mostRecentSnapshot ? simpleItemAnalysis.mostRecentSnapshot.lowTime : 0} 
                        prediction={simpleItemAnalysis.prediction}
                    />
                </List.Item>
            ))}
        </List>
    )
}